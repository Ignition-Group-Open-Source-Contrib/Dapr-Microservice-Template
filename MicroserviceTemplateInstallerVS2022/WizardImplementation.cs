using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using EnvDTE;

namespace MicroserviceTemplateInstallerVS2022
{
    public class WizardImplementation : IWizard
    {
        private UserInputForm inputForm;
        private string applicationName;

        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                // Display a form to the user. The form collects
                // input for the custom message.
                inputForm = new UserInputForm();
                inputForm.ShowDialog();

                applicationName = UserInputForm.ApplicationName;

                // Add custom parameters.
                replacementsDictionary.Add("$daprAppName$", applicationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
    public partial class UserInputForm : Form
    {
        private static string appName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;

        public UserInputForm()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();


            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Name";
           
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 20);
            this.textBox1.TabIndex = 2;

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Create Template";
            this.button1.UseVisualStyleBackColor = true;

            button1.Click += button1_Click;
            this.Size = new System.Drawing.Size(494, 275);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "DAPR deployment Settings";
        }
        public static string ApplicationName
        {
            get
            {
                return appName.ToLower();
            }
            set
            {
                appName = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationName = textBox1.Text;

            this.Close();
        }
    }
}
