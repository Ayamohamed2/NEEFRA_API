namespace NEEFRA.Core.DTO.Group
{
    public class AddMembersDTO
    {
        public string GroupId { get; set; }
        public List<string> UserIds { get; set; }
    }
}
