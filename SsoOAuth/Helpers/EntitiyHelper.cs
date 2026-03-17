namespace SsoOAuth.Helpers;

public static class EntityHelper
{
    /*public static bool AreEntitiesEquals(this BaseEntity expectedEntity, BaseEntity actualEntity, List<string> skippedFields = null, bool isCheckFieldsWithNullValue = false)
    {
        var fieldsForCheck = GetFieldsNameForCheck(expectedEntity, actualEntity, skippedFields, isCheckFieldsWithNullValue);
        return !fieldsForCheck.Any(name =>
        {
            dynamic expected = expectedEntity[name];
            dynamic actual = actualEntity[name];
            if (expected is BaseEntity)
            {
                return !AreEntitiesEquals(expected, actual);
            }
            else
            { 
                return !expected.Equals(actual);
            }
        });
    }*/
}
