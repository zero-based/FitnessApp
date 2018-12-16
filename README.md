# Fitness and Weight Loss App

This Project is still under development

**[ IMPORTANT ]** You will need [ServerDetails.cs](https://github.com/MichaelSafwatHanna/fitness-and-weight-loss-app/blob/aaa4fd2459520c5c90d143d34a0f74a2f12f98df/FitnessApp/SQLdatabase/ServerDetails.cs) if you don't have it on your local repo.
- In the top right, right click the **Raw** button.
- Save as...
- Save it in the following path: ```FitnessApp\SQLdatabase```

## I. Commits Guidelines

### Commit

- Commits **shouldn't contain multiple unrelated changes**; try and make piecemeal changes if you can, to make it easier to review and merge. In particular, don't commit style/whitespace changes and functionality changes in a single commit.
- Modify **one file** per commit. This will make merging and pulling easier for everyone.
- Make sure that the App still **runs** before making a commit.

### Commit Message

- Separate subject from body with a **blank line**.
- **Do not** end the subject line with a **period**.
- **Capitalize** the subject line and each paragraph.
- Use the **imperative mood** in the subject line.
- Wrap lines of the body at **72 characters**.
- **Asterisks** are used for the bullets in message's body.
- **Punctuate** your commit message's body.
- Add **two blank lines** followed by **Co-authors**, if found, at the end of your commit message.
- Example:

``` unix shell

Add comments and other XAML code edits

 * Add x:Name attribute to all fields and buttons.
 * Use Pascal Case in naming instead of Camel Case.
 * Add Comments to make it easier to read the code.
 * Add whitespaces between code blocks.


Co-authored-by: Michael Safwat <michaelsafwat.hanna@gmail.com>
Co-authored-by: Micheline Medhat <MichelineMedhat@gmail.com>
Co-authored-by: Mina Ossama <mina.oss.tadros@gmail.com>
Co-authored-by: Monica Atef <monicaatef46@gmail.com>
Co-authored-by: Youssef Raafat <YoussefRaafatNasry@gmail.com>

```

### Submitting Pull Requests

1. [Fork](https://github.com/MichaelSafwatHanna/fitness-and-weight-loss-app/fork) and clone the repository.
1. Create a new branch based on `master`: `git checkout -b <my-branch-name>`
1. Make your changes, and make sure the app still runs.
1. Push to your fork and [submit a pull request](https://github.com/MichaelSafwatHanna/fitness-and-weight-loss-app/compare) from your branch to `master`
1. Pat yourself on the back and wait for your pull request to be reviewed.

1. *Here are a few things you have to do:*
   - Write a good commit message.
   - Follow the style guide where possible.
   - Keep your change as focused as possible. If there are multiple changes you would like to make that are not dependent upon each other, consider submitting them as  separate pull requests.

---

## II. XAML Coding Guidelines

### Naming

- Name elements with the ```x:Name``` attribute.
- Use Pascal Casing *(i.e. first char of each word should be capitalized)*.
- Suffix XAML names with a type indication.
- Example:

``` XAML

x:Name = "EmailTextBox"

```

### Spacing

- If the tag is self closing, **leave a whitespace** before closing it.
- Else, just close it *(without any whitespaces)*.

``` XAML

<Grid Width="500">
    <TextBlock Text="Some Text" />
</Grid>

```

### Code readability

- Put one attribute per line.
- Put the first attribute on the element line.
- Put the ```x:Name``` or ```x:Key``` as the first attribute.
- Put the attached properties at the beginning of the element, eventually after the ```x:Name``` or ```x:Key```
- Order Attributes in the following order:

``` XAML

1. x:Key and x:Name
 
2. Attached properties
    - Grid.Row
    - Grid.Column
    - Grid.RowSpan
    - Grid.ColumnSpan
 
3. Positioning
    - HorizontalAlignment
    - VerticalAlignment
    - Margin
    - Padding
    - Stretch
    - Canvas
    - Grid
 
4. Box model
   - Width (always first)
   - Height
 
5. Typography
    - FontFamily
    - FontWeight
    - FontSize
    - Foreground (if related to text item)
    - Text
    - Content
    - TextWrapping
 
6. Visual
    - Background
    - Fill
    - Foreground
    - BorderBrush
    - BorderThickness
    - Stroke
    - StrokeThickness
    - Opacity
    - Visibility
    - Style
 
7. Misc
    - Material Design

8. Commands

9. Event handlers

```

---

## III. UI Guidelines

### Layout

- Use the **Grid** layout to make margins.
- Add **15px** spacing between rows and columns.
- Add **25px** whitespace border in cards and windows.

### Typography

- Use **Product Sans** Font for buttons and headings.
- Else, use **Roboto** Font.

---

## IV. C# coding guidelines

### Variables Naming Conventions

| Object Name               | Notation   |
|:--------------------------|:-----------|
| Class name                | PascalCase |
| Constructor name          | PascalCase |
| Function name             | PascalCase |
| Constants name            | PascalCase |
| Properties name           | PascalCase |
| Private variables         | _camelCase |
| Function arguments        | camelCase  |
| Local variables           | camelCase  |

### Layout Conventions

- Write only one statement per line.
- Write only one declaration per line.
- If continuation lines are not indented automatically, indent them one tab stop (four spaces).
- Add at least one blank line between method definitions and property definitions.
- Use parentheses to make clauses in an expression apparent, as shown in the following code.

```C#

if ((val1 > val2) && (val1 > val3))
{
    // Take appropriate action.
}

```

### Commenting Conventions

Place the comment on a separate line, not at the end of a line of code.
Begin comment text with an uppercase letter.
End comment text with a period.
Insert one space between the comment delimiter (//) and the comment text, as shown in the following example.

```C#

// The following declaration creates a query. It does not run
// the query.

```

### Class ordering

1. Constant Fields\variables
1. Fields\variables
1. Constructors
1. Destructors
1. Properties
1. Methods

### More Guidelines References

- Check the following [Link](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).