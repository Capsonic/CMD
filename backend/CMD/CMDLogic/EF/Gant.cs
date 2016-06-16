namespace CMDLogic.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Gant")]
    public partial class Gant
    {
        public Gant()
        {
            sys_active = true;
        }

        [Key]
        public int GantKey { get; set; }

        public int InitiativeKey { get; set; }

        public string GantData { get; set; }

        public bool sys_active { get; set; }

        public virtual Initiative Initiative { get; set; }
    }
}
