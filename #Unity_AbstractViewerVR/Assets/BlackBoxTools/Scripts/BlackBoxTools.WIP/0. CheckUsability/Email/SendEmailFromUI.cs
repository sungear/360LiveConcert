using UnityEngine;
using UnityEngine.UI;

namespace BlackBoxTools.WIP
{
public class SendEmailFromUI : MonoBehaviour {

    public InputField _from;
    public InputField _password;
    public InputField _to;
    public InputField _subject;
    public InputField _message;
    
	public void SendMail () {
        EmailSender.SendTo(_from.text, _password.text, _to.text.Split(' '), _subject.text, _message.text);

	}
}
}