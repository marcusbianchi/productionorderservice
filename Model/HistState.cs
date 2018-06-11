using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace productionorderservice.Model
{
    public class HistState 
    {
        [Key]
        public int histStatesId{get;set;}
        public int productionOrderId{get;set;}
        public string state{get;set;}
        public long date {get;set;}
        public string username{get;set;}

    }
}