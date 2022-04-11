using AutoMapper;

namespace Core.Testing.Tests.Mocks
{
    public class SeededTestValueResolver : IValueResolver<Test, TestDto, string>
    {
        private readonly ISeededTestValue _seededTestValue;

        public SeededTestValueResolver(ISeededTestValue seededTestValue)
        {
            _seededTestValue = seededTestValue;
        }
        public string Resolve(Test source, TestDto destination, string destMember, ResolutionContext context)
        {
            return _seededTestValue.Value;
        }
    }

    public interface ISeededTestValue
    {
        string Value { get; set; }
    }
}