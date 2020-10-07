using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace CrudMysqlVSCODE.Shared
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Produto
    {
        public int Id { get; set; }
        [Required]

        public float valor { get; set; }
        [Required]

        public string descricao { get; set; }
        
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}