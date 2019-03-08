using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetSuppliesPlus.Framework;

namespace PetSuppliesPlus.Models
{
    /// <summary>
    /// to manage application transaction message
    /// </summary>
    public class TransactionMessage
    {
        public string Message { get; set; }
        public MessageStatus Status { get; set; }        
    }
}