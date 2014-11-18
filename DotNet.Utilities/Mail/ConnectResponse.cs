/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.IO;

namespace DotNet.Utilities
{
    internal sealed class ConnectResponse : Pop3Response
    {
        private Stream _networkStream;
        public Stream NetworkStream
        {
            get
            {
                return _networkStream;
            }
        }

        public ConnectResponse(Pop3Response response, Stream networkStream)
            : base(response.ResponseContents, response.HostMessage, response.StatusIndicator)
        {
            if (networkStream == null)
            {
                throw new ArgumentNullException("networkStream");
            }
            _networkStream = networkStream;
        }
    }
}
