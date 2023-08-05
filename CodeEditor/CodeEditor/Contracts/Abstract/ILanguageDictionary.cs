using System.Collections.Generic;

namespace CodeEditor.Contracts.Abstract;

public interface ILanguageDictionary
{
   Dictionary<string, string> Languages { get; }
}