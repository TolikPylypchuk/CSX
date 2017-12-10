# CSX

Functional Extensions for C#

*Note:* I started writing this library, because I was bored.
So I don't guarantee anything. Also, this project uses
some features of C# 7.2, like the private-protected modifier.

This library provides data structures usually used in
functional programming, such as options, cons cells,
computation results etc.

I chose to implement those structures using inheritance,
with the base class being the structure, and subclasses
its cases. I'm not sure it's the best way to do it,
but it lets polymorphism deal with choosing the right cases
thus eliminating a lot of boilerplate. And it's similar
to Scala's sealed trait/final case classes pattern of
defining union types.

For more information take a look at the
[wiki](https://github.com/TolikPylypchuk/CSX/wiki).
