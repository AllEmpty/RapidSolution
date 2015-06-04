﻿/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// This class represents the Pop3 QUIT command.
    /// </summary>
    internal sealed class QuitCommand : Pop3Command<Pop3Response>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuitCommand"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public QuitCommand(Stream stream)
            : base(stream, false, Pop3State.Transaction | Pop3State.Authorization) { }

        /// <summary>
        /// Creates the Quit request message.
        /// </summary>
        /// <returns>
        /// The byte[] containing the QUIT request message.
        /// </returns>
        protected override byte[] CreateRequestMessage()
        {
            return GetRequestMessage(Pop3Commands.Quit);
        }
    }
}
