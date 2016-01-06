using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace eXpressPrint
{
    public partial class WaitForm1 : WaitForm
    {
         
        public WaitForm1()
        {
            InitializeComponent();

            this.ShowOnTopMode = ShowFormOnTopMode.AboveAll;
            this.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
