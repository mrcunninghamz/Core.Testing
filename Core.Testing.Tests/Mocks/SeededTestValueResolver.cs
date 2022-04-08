using AutoMapper;

namespace Core.Testing.Tests.Mocks
{
    public class SeededTestValueResolver : IValueResolver<Test, TestDto, string>
    {
        private readonly string _seededTestValue;

        public SeededTestValueResolver(ISeededTestValue seededTestValue)
        {
            _seededTestValue = seededTestValue.Value;
        }
        public string Resolve(Test source, TestDto destination, string destMember, ResolutionContext context)
        {
            return _seededTestValue;
        }
    }

    public interface ISeededTestValue
    {
        string Value { get; set; }
    }
}