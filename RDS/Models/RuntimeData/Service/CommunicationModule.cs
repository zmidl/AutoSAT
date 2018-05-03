using RDCM;
using System;

namespace RDS.Models.RuntimeData.Service
{
    public class CommunicationModule : Module
    {
        public event EventHandler<string> DataReceived;

        public CommunicationModule(string strModuleName) : base(strModuleName) { }

        public override int DoRecive(string strData, uint nUserFlag, string strForm, string strTo, object objCallback)
        {
            this.DataReceived(this, strData);
            return base.DoRecive(strData, nUserFlag, strForm, strTo, objCallback);
        }
    }
}
