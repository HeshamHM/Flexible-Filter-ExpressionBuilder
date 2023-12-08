
using System.Linq.Expressions;

namespace Expression_Builder
{
    public class UserExpressionBuilder
    {
        private readonly ParameterExpression userParameter;
        private Expression<Func<User, bool>> baseExpression;

        private UserExpressionBuilder(ParameterExpression parameter)
        {
            userParameter = parameter;
            baseExpression = UserExpression.SelectAll();
        }

        public static UserExpressionBuilder CreateInstance(ParameterExpression parameter)
        {
            return new UserExpressionBuilder(parameter);
        }

        public UserExpressionBuilder AddFilter(Expression filterExpression)
        {
            baseExpression = UserExpression.AddExpression(userParameter, filterExpression, baseExpression);
            return this;
        }

        public UserExpressionBuilder AddActiveFilter(bool isActive)
        {
            return AddFilter(UserExpression.ActiveFilter(userParameter, isActive));
        }

        public UserExpressionBuilder AddSearchExpression(string searchTerm)
        {
            return AddFilter(UserExpression.SearchExpression(userParameter, searchTerm));
        }
        public UserExpressionBuilder AddCountryFilter(string country)
        {
            return AddFilter(UserExpression.CountryFilter(userParameter, country));
        }

        public Expression<Func<User, bool>> Build()
        {
            return baseExpression;
        }

        private static class UserExpression
        {
            public static Expression<Func<User, bool>> SelectAll()
            {
                return entity => true;
            }

            public static Expression<Func<User, bool>> AddExpression(ParameterExpression userParameter, Expression expression, Expression baseExpression)
            {
                return Expression.Lambda<Func<User, bool>>(
                    Expression.AndAlso(Expression.Invoke(baseExpression, userParameter), expression),
                    userParameter
                );
            }

            public static BinaryExpression ActiveFilter(ParameterExpression userParameter, bool isActive)
            {
                var isActiveProperty = Expression.Property(userParameter, "IsActive");
                var value = Expression.Constant(isActive, typeof(bool));
                return Expression.Equal(isActiveProperty, value);
            }

            public static BinaryExpression SearchExpression(ParameterExpression userParameter, string searchTerm)
            {
                var nameProperty = Expression.Property(userParameter, "Name");
                var emailProperty = Expression.Property(userParameter, "Email");

                var nameToLower = CreateStringContainsExpression(nameProperty, searchTerm);
                var emailToLower = CreateStringContainsExpression(emailProperty, searchTerm);

                return Expression.OrElse(nameToLower, emailToLower);
            }
            public static BinaryExpression CountryFilter(ParameterExpression userParameter, string country)
            {
                var countryProperty = Expression.Property(userParameter, "Country");
                var countryToLower = CreateStringEqualsExpression(countryProperty, country);

                return countryToLower;
            }
            private static MethodCallExpression CreateStringContainsExpression(MemberExpression property, string searchTerm)
            {
                var toLowerExpression = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
                return Expression.Call(toLowerExpression, "Contains", null, Expression.Constant(searchTerm.ToLower()));
            }
            private static BinaryExpression CreateStringEqualsExpression(MemberExpression property, string value)
            {
                var valueToLower = Expression.Constant(value.ToLower());
                return Expression.Equal(Expression.Call(property, "ToLower", null), valueToLower);
            }
        }
    }
}
