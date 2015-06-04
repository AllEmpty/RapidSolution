/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// This command represents the Pop3 RSET command.
    /// </summary>
    internal sealed class RsetCommand : Pop3Command<Pop3Response>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsetCommand"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public RsetCommand(Stream stream)
            : base(stream, false, Pop3State.Transaction) { }

        /// <summary>
        /// Creates the RSET request message.
        /// </summary>
        /// <returns>
        /// The byte[] containing the RSET request message.
        /// </returns>
        protected override byte[] CreateRequestMessage()
        {
            return GetRequestMessage(Pop3Commands.Rset);
        }
    }
}
