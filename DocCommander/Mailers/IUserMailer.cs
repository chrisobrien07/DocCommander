using Mvc.Mailer;
using DocCommander.Models;

namespace DocCommander.Mailers
{ 
    public interface IUserMailer
    {
        MvcMailMessage ConfirmationTokenMessage(EmailConfirmationModel model);
        MvcMailMessage PasswordResetMessage(EmailPasswordResetModel model);
	}
}