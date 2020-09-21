# Examine Facets

Powerful filtering and faceting directly within the [Examine](https://github.com/shazwazza/examine) fluent API.

| Package                    | NuGet             |
|----------------------------|-------------------|
| Examine.Facets             | [![NuGet](https://img.shields.io/nuget/v/Examine.Facets.svg)](https://www.nuget.org/packages/Examine.Facets/) |
| Examine.Facets.BoboBrowse  | [![NuGet](https://img.shields.io/nuget/v/Examine.Facets.BoboBrowse.svg)](https://www.nuget.org/packages/Examine.Facets.BoboBrowse/) |
| Examine.Facets.MultiFacets | [![NuGet](https://img.shields.io/nuget/v/Examine.Facets.MultiFacets.svg)](https://www.nuget.org/packages/Examine.Facets.MultiFacets/) |

## Getting started

This package requires [Examine](https://github.com/shazwazza/examine) 1.1.0+.

### Installation

Examine Facets is available from NuGet, or as a manual download directly from GitHub.

#### NuGet package repository

To [install from NuGet](https://www.nuget.org/packages/Examine.Facets/), run the following command in your instance of Visual Studio.

    PM> Install-Package Examine.Facets

## Usage

Examine Facets integrates seamlessly with the Examine API. *Read the [Examine docs](https://shazwazza.github.io/Examine/) first.*

### Register the searcher

There are 2 facet engines available out of the box: [Bobo Browse](https://www.nuget.org/packages/Examine.Facets.BoboBrowse/) and [Multi Facets](https://www.nuget.org/packages/Examine.Facets.MultiFacets/). Both offer a similar choice of features and performance so it is really a matter of preference.

To perform facet queries the chosen facet engine's Searcher must be registered via `ExamineManager`. This requires only a few lines of configuration code.

For example the [Bobo Browse](https://www.nuget.org/packages/Examine.Facets.BoboBrowse/) Searcher can be registered like this:

```
if (_examineManager.TryGetIndex("CustomIndex", out IIndex index))
{
    if (index is LuceneIndex luceneIndex)
    {
        var searcher = new BoboFacetSearcher(
            "FacetSearcher",
            luceneIndex.GetIndexWriter(),
            luceneIndex.DefaultAnalyzer,
            luceneIndex.FieldValueTypeCollection
        );

        _examineManager.AddSearcher(searcher);
    }
}
```

### Querying

Defining and querying facets is baked right into Examine's fluent API.

Begin a facet query by calling `.Facet(string field)` within a query, or filter results to a facet with a specific value by calling `.Facet(string field, string[] values)`.

Further optional configuration – such as the minimum number of matches required for a facet to appear, or the maximum number of values to return – can also be configured configured through the fluent API.

```
_examineManager.TryGetSearcher("FacetSearcher", out ISearcher searcher);

var query = searcher.CreateQuery();

query.And()
    .Facet("CustomField")
        .MinHits(10)
        .MaxCount(100);
```

### Results

Facet searches behave the same as any other Examine search. To retreive information about facets there are some handy extension methods.

```
var results = searcher.Execute();
```

To get a list of all facets:

```
results.GetFacets();
```

To get a list of values for a specific facet:

```
results.GetFacet(string field);
```

To get the number of hits for a specific value:

```
results
    .GetFacet(string field)
    .GetHits(object value);
```

## Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the library.

### Who do I talk to?

This project is maintained by [Callum Whyte](https://callumwhyte.com/) and contributors. If you have any questions about the project please get in touch on [Twitter](https://twitter.com/callumbwhyte), or by raising an issue on GitHub.

## Credits

[Examine](https://github.com/shazwazza/examine) was created by [Shannon Deminick](https://github.com/shazwazza) and is licensed under [Microsoft Public License (MS-PL)](https://opensource.org/licenses/ms-pl).

[BoboBrowse.Net](https://github.com/NightOwl888/BoboBrowse.Net) was created by [Shad Storhaug](https://github.com/NightOwl888) and is licensed under [Apache License 2.0](https://github.com/NightOwl888/BoboBrowse.Net/blob/master/LICENSE.md).

[MultiFacetLucene.Net](https://github.com/aspcodenet/MultiFacetLuceneNet) was created by [Stefan Holmberg](https://github.com/aspcodenet).

## License

Copyright &copy; 2020 [Callum Whyte](https://callumwhyte.com/), and other contributors

Licensed under the [MIT License](LICENSE.md).