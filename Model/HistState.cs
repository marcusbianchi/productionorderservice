using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace productionorderservice.Model
{
    public class HistState 
    {
        [Key]
        public int histStatesId{get;set;}
        public int productionOrderId{get;set;}
        public string state{get;set;}
        public long date {get;set;}     
        [NotMapped]   
        public string username{get;set;}

    }
}