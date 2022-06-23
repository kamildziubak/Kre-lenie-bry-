
namespace OOP_Projekt3_Dziubak59363
{
    partial class kdFormPowitalny
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.kdBtnLaboratorium = new System.Windows.Forms.Button();
            this.kdBtnProjekt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(98, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(579, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wizualizacja brył geometrycznych";
            // 
            // kdBtnLaboratorium
            // 
            this.kdBtnLaboratorium.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kdBtnLaboratorium.Location = new System.Drawing.Point(105, 161);
            this.kdBtnLaboratorium.Name = "kdBtnLaboratorium";
            this.kdBtnLaboratorium.Size = new System.Drawing.Size(290, 133);
            this.kdBtnLaboratorium.TabIndex = 1;
            this.kdBtnLaboratorium.Text = "Laboratorium\r\nBryły regularne";
            this.kdBtnLaboratorium.UseVisualStyleBackColor = true;
            this.kdBtnLaboratorium.Click += new System.EventHandler(this.kdBtnLaboratorium_Click);
            // 
            // kdBtnProjekt
            // 
            this.kdBtnProjekt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kdBtnProjekt.Location = new System.Drawing.Point(401, 161);
            this.kdBtnProjekt.Name = "kdBtnProjekt";
            this.kdBtnProjekt.Size = new System.Drawing.Size(290, 133);
            this.kdBtnProjekt.TabIndex = 2;
            this.kdBtnProjekt.Text = "Projekt nr 3\r\nBryły złożone";
            this.kdBtnProjekt.UseVisualStyleBackColor = true;
            this.kdBtnProjekt.Click += new System.EventHandler(this.kdBtnProjekt_Click);
            // 
            // kdFormPowitalny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kdBtnProjekt);
            this.Controls.Add(this.kdBtnLaboratorium);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "kdFormPowitalny";
            this.ShowIcon = false;
            this.Text = "Wizualizacja brył geometrycznych";
            this.Load += new System.EventHandler(this.kdFormPowitalny_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button kdBtnLaboratorium;
        private System.Windows.Forms.Button kdBtnProjekt;
    }
}

