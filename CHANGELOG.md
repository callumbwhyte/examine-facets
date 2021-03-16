# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/) and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.1.0] - 2021-03-16
### Changed
* Minimum required version of Examine is now 1.1.0+
* Sorting and paging is now handled by Examine

### Fixed
* `MinHits` and `MaxCount` weren't being applied when creating facets
* `GetHits` was returning `0` for any given value

### Removed
* `FacetSearchResultsBase` has been replaced by Examine 1.1.0

## [1.0.0] - 2020-08-12
### Added
* Initial release of Examine Facets
* Support for BoboBrowse.Net
* Support for MultiFacetsLucene.Net
* Documentation for configuration and querying

[Unreleased]: https://github.com/callumbwhyte/examine-facets/compare/release-1.1.0...HEAD
[1.1.0]: https://github.com/callumbwhyte/examine-facets/compare/release-1.0.0...release-1.1.0
[1.0.0]: https://github.com/callumbwhyte/examine-facets/tree/release-1.0.0