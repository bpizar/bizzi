using JayGor.People.Entities.Entities;


namespace JayGor.People.Entities.CustomEntities
{
    public class Identity_RolesCustom : identity_roles
	{
        public bool IsInrole { get; set; }
		public string Abm { get; set; }
        public string Group { get; set; }

        //public long Id { get; set; }
        //public string Rol { get; set; }
        //public string State { get; set; }
        //public string DisplayShortName { get; set; }
        //public string RolDescription { get; set; }
    }
}
