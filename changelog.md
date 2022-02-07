# Changelog

## 0.1.0
Initial release

## 0.2.0
* Convert configuration PODs from structs to classes.
* Use `Funcky.Option` instead of nullable types.

## 0.2.1
* `[Pure]` attributes are now emitted correctly.
* Relax version range for `System.Collections.Immutable`.
* Drop explicit support for `netstandard2.1` in favor of `net5.0`.
  Consumers who need `netstandard2.1` will fall back to `netstandard2.0`.

## 0.2.2
* Publish symbols package.
