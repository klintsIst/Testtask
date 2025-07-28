namespace AGSRTestTask.Common.Helpers;

using Common.Enums;
using Common.Models.PatientModels;
using System.Linq.Expressions;

public static class FHIRQueryParser
{
    public static Expression<Func<Patient, bool>> Parse(string query)
    {
        // TODO: Add operators parsing.
        if (query.Contains('&') || query.Contains(','))
        {
            throw new NotImplementedException("FHIR operators have not implemented yet.");
        }

        Expression<Func<Patient, bool>>? parsedQuery = ParsePrefixes(query);

        return parsedQuery;
    }

    private static Expression<Func<Patient, bool>> ParseOperators(string query)
    {
        throw new NotImplementedException();
    }

    private static Expression<Func<Patient, bool>> ParsePrefixes(string query)
    {
        Expression<Func<Patient, bool>>? resultExpr = null;
        IEnumerable<FHIRPrefix> FHIRPrefixes = Enum.GetValues(typeof(FHIRPrefix)).Cast<FHIRPrefix>();

        foreach (FHIRPrefix prefix in FHIRPrefixes)
        {
            int prefixIndex = query.IndexOf(prefix.ToString());
            DateTime? value = null;
            if (prefixIndex == -1)
            {
                continue;
            }
            else
            {
                string rawValue = query.Substring(prefixIndex + prefix.ToString().Length);
                if (string.IsNullOrEmpty(rawValue))
                {
                    throw new Exception("Can not parse value in query.");
                }
                value = DateTime.Parse(rawValue);
            }

            resultExpr = prefix switch
            {
                FHIRPrefix.eq => p => p.BirthDate.Equals(value),
                FHIRPrefix.ne => p => !p.BirthDate.Equals(value),
                FHIRPrefix.gt => p => p.BirthDate > value,
                FHIRPrefix.lt => p => p.BirthDate < value,
                FHIRPrefix.ge => p => p.BirthDate >= value,
                FHIRPrefix.le => p => p.BirthDate >= value,
                FHIRPrefix.sa => throw new NotImplementedException(),
                FHIRPrefix.eb => throw new NotImplementedException(),
                FHIRPrefix.ap => throw new NotImplementedException(),
                _ => throw new NotImplementedException("Found not supported prefix."),
            };
        }

        if (resultExpr is null)
        {
            throw new Exception("Can not parse query.");
        }

        return resultExpr;
    }
}
