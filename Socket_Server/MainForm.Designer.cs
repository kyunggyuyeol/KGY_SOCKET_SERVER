namespace Socket_Server
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_log = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_log
            // 
            this.lb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_log.FormattingEnabled = true;
            this.lb_log.ItemHeight = 12;
            this.lb_log.Location = new System.Drawing.Point(0, 0);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(729, 435);
            this.lb_log.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(729, 435);
            this.Controls.Add(this.lb_log);
            this.Name = "MainForm";
            this.Text = "Socket_Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_log;

    }
}

