using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using System.Linq.Expressions;
using System.Security.Cryptography;

// From my research, I couldn't find a function within C# to return the equivalent of "netstat -ano" I had to make use
// # of a system C function call to a dll. This class will wrap that code so I can make use of it elsewhere
namespace port_man
{
    public class EstablishedConnectionData
    {
        public string m_local_address { get; set; }
        public int m_local_port { get; set; }
        public string m_remote_address { get; set; }
        public int m_remote_port { get; set; }
        public int m_pid { get; set; }
        public string m_process_name { get; set; }
        public string m_dns { get; set; }


        public void PrintData()
        {
            Console.WriteLine($"Local Address: {m_local_address}:{m_local_port}, Remote Address: {m_remote_address}:{m_remote_port}, Process ID: {m_pid}, ProcessName: {m_process_name}");
        }

        public string GetLocalAddress()
        {
            return m_local_address + " : " + m_local_port.ToString();
        }

        public string GetRemoteAddress()
        {
            return m_remote_address + " : " + m_remote_port.ToString();
        }

        public string GetKey()
        {
            return m_local_address + "_" + m_local_port.ToString() + "_" + m_pid.ToString();
        }
    }

    internal class NetstatHelper
    {
        Dictionary<string, string> m_dns_lookup_map = new Dictionary<string, string>();

        const int AF_INET = 2;  // IPv4
        const int TCP_TABLE_OWNER_PID_ALL = 5;  // Extended TCP table with PID information

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public uint dwState;
            public uint dwLocalAddr;
            public uint dwLocalPort;
            public uint dwRemoteAddr;
            public uint dwRemotePort;
            public uint dwOwningPid;
        }

        [DllImport("iphlpapi.dll", SetLastError = true)]
        public static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int dwSize, bool bOrder, int ulAf, int TableClass, int Reserved);

        public List<EstablishedConnectionData> RetrieveTcpInformation()
        {
            var connList = new List<EstablishedConnectionData>();
            IntPtr pTable = IntPtr.Zero;
            int dwSize = 0;

            // Get the size of the table
            int result = GetExtendedTcpTable(IntPtr.Zero, ref dwSize, true, AF_INET, TCP_TABLE_OWNER_PID_ALL, 0);

            try
            {
                // Allocate memory for the table
                pTable = Marshal.AllocHGlobal(dwSize);
                result = GetExtendedTcpTable(pTable, ref dwSize, true, AF_INET, TCP_TABLE_OWNER_PID_ALL, 0);

                if (result == 0)
                {
                    int rowSize = Marshal.SizeOf(typeof(MIB_TCPROW_OWNER_PID));
                    int rowCount = Marshal.ReadInt32(pTable);

                    IntPtr currentRowPtr = pTable + sizeof(int);
                    for (int i = 0; i < rowCount; i++)
                    {
                        MIB_TCPROW_OWNER_PID row = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(currentRowPtr, typeof(MIB_TCPROW_OWNER_PID));
                        if (row.dwState == 5)
                        {
                            var process = Process.GetProcessById((int)row.dwOwningPid);
                            //Console.WriteLine($"Local Address: {IPAddressFromUInt(row.dwLocalAddr)}:{row.dwLocalPort}, Remote Address: {IPAddressFromUInt(row.dwRemoteAddr)}:{row.dwRemotePort}, State: {row.dwState}, Process ID: {row.dwOwningPid}, ProcessName: {process.ProcessName}");
                            //Console.WriteLine($"Local Address: {IPAddressFromUInt(row.dwLocalAddr)}:{row.dwLocalPort});
                            //Console.WriteLine($"Remote Address: {IPAddressFromUInt(row.dwRemoteAddr)}:{row.dwRemotePort}");
                            //Console.WriteLine($"State: {row.dwState}");
                            //Console.WriteLine($"Process ID: {row.dwOwningPid}");
                            //Console.WriteLine();
                            EstablishedConnectionData data = new EstablishedConnectionData();
                            data.m_local_address = IPAddressFromUInt(row.dwLocalAddr);
                            data.m_local_port = (int)row.dwLocalPort;
                            data.m_remote_address = IPAddressFromUInt(row.dwRemoteAddr);
                            data.m_remote_port = (int)row.dwRemotePort;
                            data.m_pid = (int)row.dwOwningPid;
                            data.m_process_name = process.ProcessName;
                            data.m_dns = "Unknown";

                            /* DNS lookups are very slow so lets disable this for now
                            try
                            {
                                if (m_dns_lookup_map.ContainsKey(data.m_remote_address))
                                    data.m_dns = m_dns_lookup_map[data.m_remote_address];
                                else
                                {
                                    // Lets find it and store it
                                    IPHostEntry entry = Dns.GetHostEntry(data.m_remote_address);
                                    string dns = entry.HostName;
                                    data.m_dns = dns;
                                    m_dns_lookup_map[data.m_remote_address] = dns;
                                }
                            }
                            catch(System.ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                data.m_dns = "Unknown";
                                m_dns_lookup_map[data.m_remote_address] = "Unknown";
                            }
                            catch(System.Net.Sockets.SocketException ex)
                            {
                                Console.WriteLine(ex.Message);
                                data.m_dns = "Unknown";
                                m_dns_lookup_map[data.m_remote_address] = "Unknown";
                            }
                            */

                            connList.Add(data);
                        }

                        currentRowPtr += rowSize;
                    }
                }
                else
                {
                    Console.WriteLine("Error retrieving TCP table.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // As we were parsing the process information object some closed the application so we need to handle it
                Console.WriteLine(ex.Message);
                Marshal.FreeHGlobal(pTable);
            }
            finally
            {
                Marshal.FreeHGlobal(pTable);
            }

            return connList;
        }

        static string IPAddressFromUInt(uint ipAddr)
        {
            return new IPAddress(BitConverter.GetBytes(ipAddr)).ToString();
        }

    }
}
