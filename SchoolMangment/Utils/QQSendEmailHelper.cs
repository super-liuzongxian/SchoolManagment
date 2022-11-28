using Newtonsoft.Json.Linq;
using SchoolMangment.Models;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace SchoolMangment.Utils
{
    public class QQSendEmailHelper : AbstractSendEmailHelper
    {
        public override bool Send()
        {
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            //指定电子邮件发送方式
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //指定SMTP服务器
            smtpClient.Host = "smtp.qq.com";
            //使用安全加密连接（是否启用SSL）
            smtpClient.EnableSsl = true;
            //不和请求一块发送。
            smtpClient.UseDefaultCredentials = false;
            //用户名和密码
            smtpClient.Credentials = new NetworkCredential(MailFrom, Token);

            // 发送人和收件人
            MailMessage mailMessage = new MailMessage(MailFrom, MailTo);
            //主题
            mailMessage.Subject = Subject;
            //内容
            mailMessage.Body = Body;
            //正文编码
            mailMessage.BodyEncoding = Encoding.UTF8;
            //设置为HTML格式
            mailMessage.IsBodyHtml = true;
            //优先级
            mailMessage.Priority = MailPriority.Low;
            try
            {
                // 发送邮件
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (SmtpException ex)
            {
                //打印错误
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
