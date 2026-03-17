namespace SsoOAuth.BaseClasses.Entities.API;

public class SubscriberGroupsEntity
{
    public int GroupSubscriberId { get; set; }
    public int FbGroupId { get; set; }
    public int SubscriberId { get; set; }
    public string OptInDt { get; set; }
    public string OptOutDt { get; set; }
    public string GroupName { get; set; }
    public int? ParentGroupId { get; set; }
    public string ParentGroupName { get; set; }
    public bool? Disconnected { get; set; }
    public string ExternalSubscriberCode { get; set; }
}
    
public class SubscriberGroupsList
{
    public List<SubscriberGroupsEntity> GroupList { get; set; }
    public Dictionary<string, ErrorResponseEntity> ErrorResponses { get; set; }
}
