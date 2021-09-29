using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AutoLink
{
        public class ComAPI
        {
            /// <summary>
            /// 向TCP服务器请求建立TCP连接，当连接建立后，通过OnAccept回调函数通知用户程序。使用该方法时必须将对端的C2000设置成“将C2000作为TCP服务器”。
            /// </summary>
            /// <param name="PeerIP">C2000的IP地址</param>
            /// <param name="PeerPort">C2000的监听端口号</param>
            /// <param name="LocalIP">本机的IP地址</param>
            /// <param name="LocalPort">本机用于连接的端口号。如果为零，则由系统自动选择一个端口号</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool OpenConnect(string PeerIP, int PeerPort, string LocalIP, int LocalPort);

            /// <summary>
            /// 关闭所有的连接
            /// </summary>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool CloseAllConnect();

            /// <summary>
            /// 关闭指定的连接
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool CloseConnect(int ConnectID);

            /// <summary>
            /// 开始监听
            /// </summary>
            /// <param name="LocalIP">本地IP</param>
            /// <param name="LocalPort">本地端口号</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool StartListen(string LocalIP, int LocalPort);

            /// <summary>
            /// 停止监听
            /// </summary>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool StopListen();

            /// <summary>
            /// 设置回调事件
            /// </summary>
            /// <param name="lpOnAccept">建立连接回调事件</param>
            /// <param name="lpOnConnectClose">关闭连接回调事件</param>
            /// <param name="lpOnError">产生错误回调事件</param>
            /// <param name="lpOnReceConfigData">设置数据回调事件</param>
            /// <param name="lpOnReceFromCOM">从串口接收数据回调事件</param>
            /// <param name="lpOnSwitchChange">开关量输入端口的电平变化回调事件</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool SetCallback(OnAccept lpOnAccept, OnConnectClose lpOnConnectClose, OnError lpOnError, OnReceConfigData lpOnReceConfigData, OnReceFromCOM lpOnReceFromCOM, OnSwitchChange lpOnSwitchChange);

            /// <summary>
            /// 获取MAC地址
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <param name="lpMac">MAC地址数据</param>
            /// <param name="maclen">长度</param>
            /// <returns>实际长度</returns>
            [DllImport("EdSockServer.dll")]
            public static extern int GetMAC(int ConnectID, byte[] lpMac, int maclen);

            /// <summary>
            /// 获取C2000设备的型号
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <returns>型号对应的编码</returns>
            [DllImport("EdSockServer.dll")]
            public static extern int GetModel(int ConnectID);

            /// <summary>
            /// 获取C2000的IP地址
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <returns>IP地址数据</returns>
            [DllImport("EdSockServer.dll")]
            public static extern uint GetPeerIP(int ConnectID);

            /// <summary>
            /// 获取C2000的端口号
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <returns>端口号</returns>
            [DllImport("EdSockServer.dll")]
            public static extern int GetPeerPort(int ConnectID);

            /// <summary>
            /// 获取C2000的串口号
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <returns>串口号</returns>
            [DllImport("EdSockServer.dll")]
            public static extern short GetCOM(int ConnectID);

            /// <summary>
            /// 接收数据
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <param name="pDataBuf">接收数据缓冲区</param>
            /// <param name="BufLen">数据缓冲区的长度</param>
            /// <param name="dwWait">接收超时时间(毫秒)</param>
            /// <returns>实际接收数据的长度</returns>
            [DllImport("EdSockServer.dll")]
            public static extern int ReceiveData(int ConnectID, byte[] pDataBuf, int BufLen, uint dwWait);

            /// <summary>
            /// 设置是否是透明Socket通讯
            /// </summary>
            /// <param name="IsSocketValue">为true使用透明Socket通讯,为false则不使用</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool SetSocket(bool IsSocketValue);

            /// <summary>
            /// 获取当前是否为透明的Socket通讯
            /// </summary>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool IsSocket();

            /// <summary>
            /// 向指定串口号发送数据
            /// </summary>
            /// <param name="ConnectID">连接ID</param>
            /// <param name="COMNum">串口号</param>
            /// <param name="pDataBuf">要发送的数据</param>
            /// <param name="DataLength">数据长度</param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool SendToCOM(int ConnectID, int COMNum, byte[] pDataBuf, int DataLength);

            /// <summary>
            /// 获得指定TCP连接对端C2000上开关量端口的方向
            /// </summary>
            /// <param name="ConnectID"></param>
            /// <param name="PortNum"></param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern int GetSwitchDirection(int ConnectID, int PortNum);

            /// <summary>
            /// 获得指定TCP连接对端C2000上开关量端口的输入输出电平
            /// </summary>
            /// <param name="ConnectID"></param>
            /// <param name="PortNum"></param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern int GetSwitchValue(int ConnectID, int PortNum);

            /// <summary>
            /// 向指定TCP连接对端的C2000的指定开关量端口输出高电平或低点平
            /// </summary>
            /// <param name="ConnectID"></param>
            /// <param name="PortNum"></param>
            /// <param name="Value"></param>
            /// <returns></returns>
            [DllImport("EdSockServer.dll")]
            public static extern bool SetSwitchValue(int ConnectID, int PortNum, int Value);
        }
    #region 委托
    /// <summary>
    /// 建立连接事件委托
    /// </summary>
    /// <param name="ConnectID"></param>
    public delegate void OnAccept(int ConnectID);
        /// <summary>
        /// 关闭连接事件委托
        /// </summary>
        /// <param name="ConnectID"></param>
        public delegate void OnConnectClose(int ConnectID);
        /// <summary>
        /// 产生错误数据事件委托
        /// </summary>
        /// <param name="SocketID"></param>
        /// <param name="ErrorCode"></param>
        public delegate void OnError(int SocketID, int ErrorCode);
        /// <summary>
        /// 设置数据事件委托
        /// </summary>
        /// <param name="ConnectID"></param>
        /// <param name="pDataBuf"></param>
        /// <param name="DataLength"></param>
        public delegate void OnReceConfigData(int ConnectID, byte[] pDataBuf, int DataLength);
        /// <summary>
        /// 从串口接收数据事件委托
        /// </summary>
        /// <param name="ConnectID"></param>
        /// <param name="COMNum"></param>
        /// <param name="pDataBuf"></param>
        /// <param name="DataLength"></param>
        public delegate void OnReceFromCOM(int ConnectID, int COMNum, byte[] pDataBuf, int DataLength);
        /// <summary>
        /// 开关量输入端口的电平变化事件委托
        /// </summary>
        /// <param name="ConnectID"></param>
        /// <param name="PortNum"></param>
        /// <param name="Value"></param>
        public delegate void OnSwitchChange(int ConnectID, int PortNum, int Value);
    #endregion
}
