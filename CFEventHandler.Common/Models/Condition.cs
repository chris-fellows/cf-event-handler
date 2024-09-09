using CFEventHandler.Enums;

namespace CFEventHandler.Models
{
    /// <summary>
    /// Condition. 
    /// E.g. PropertyX equals 10
    /// E.g. PropertyX between 10 and 100
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Item to evaluate
        /// </summary>
        public string ItemName { get; set; } = String.Empty;

        /// <summary>
        /// Whether to reverse condition type. 
        /// E.g. Not=true ConditionType=Equals : Not equals
        /// </summary>
        public bool Not { get; set; }

        public ConditionTypes ConditionType { get; set; }

        /// <summary>
        /// Values to check
        /// </summary>
        public List<object> Values { get; set; } = new List<object>();

        /// <summary>
        /// Join type for multiple conditions
        /// </summary>
        public ConditionJoinTypes JoinType { get; set; } = ConditionJoinTypes.And;

        /// <summary>
        /// Returns whether value meets condition
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsMeetsCondition(object value)
        {
            // TODO: Implement this
            throw new NotImplementedException();
            return false;
        }

        /// <summary>
        /// Whether value meets all conditions
        /// 
        /// Currently limited to requiring all join types to be the same in order to keep the evaluation logic
        /// simple.
        /// </summary>
        /// <param name="conditions">Conditions</param>
        /// <param name="valuesByItemName">Values by item name</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsMeetsConditions(List<Condition> conditions, Dictionary<string, object> valuesByItemName)
        {
            // For the moment keep it simple to that we only support one join type
            int countJoinTypes = conditions.Select(c => c.JoinType).Distinct().Count();
            if (countJoinTypes > 1)
            {
                throw new ArgumentException("Currently only supports one join type");
            }

            // Count up ORs and ANDs
            int countOr = conditions.Count(c => c.JoinType == ConditionJoinTypes.Or);
            int countAnd = conditions.Count(c => c.JoinType == ConditionJoinTypes.And);

            // Evaluate each condition            
            var conditionResults = conditions.Select(condition =>
            {
                return condition.IsMeetsCondition(valuesByItemName[condition.ItemName]);                
            }).ToList();

            if (countAnd > 0)   // All conditions must be true
            {
                return conditionResults.Count(r => true) == conditionResults.Count();
            }
            else    // Any condition must be true
            {
                return conditionResults.Count(r => true) > 0;
            }            
        }
    }
}
