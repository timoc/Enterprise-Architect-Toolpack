﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MP = MappingFramework;

namespace EAMapping
{
    public partial class MappingDetailsControl : UserControl
    {
        public MappingDetailsControl()
        {
            InitializeComponent();
        }
        private void loadContent()
        {
            this.fromTextBox.Text = this._mapping?.source?.name;
            this.toTextBox.Text = this.mapping != null && this.mapping.isEmpty ? 
                                    "<none>" 
                                    : this._mapping?.target?.name;
            this.mappingLogicTextBox.Text = this._mapping?.mappingLogicDescription;
            enableDisable();
        }
        private void enableDisable()
        {
            //disable delete button when mapping is readonly
            this.deleteButton.Enabled = this.mapping != null && !this._mapping.isReadOnly;
        }

        private MP.Mapping _mapping;
        public MP.Mapping mapping
        {
            get
            {
                return this._mapping;
            }
            set
            {
                this._mapping = value;
                this.loadContent();
            }
        }

        private void mappingLogicTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.mapping != null)
            {
                this.mapping.mappingLogicDescription = mappingLogicTextBox.Text;
                this.mapping.save();

            }
                
        }
        public event EventHandler mappingDeleted;
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.mappingDeleted?.Invoke(this, e);
        }
        public event EventHandler mapping_Enter;
        private void MappingDetailsControl_Enter(object sender, EventArgs e)
        {
            this.mapping_Enter?.Invoke(this, e);
        }
    }
}
