# Fitness and Weight Loss App

This Project is still under development

# Contributing Guidelines

## 1. Commit Messages Guidelines

- Separate subject from body with a **blank line**.
- **Do not** end the subject line with a **period**.
- **Capitalize** the subject line and each paragraph.
- Use the **imperative mood** in the subject line.
- Wrap lines of the body at **72 characters**.
- **Asterisks** are used for the bullets in message's body.
- **Punctuate** your commit message's body.
- Example:

```

    Add comments and other XAML code edits

     * Add x:Name attribute to all feilds and buttons.
     * Use Pascal Case in naming instead of Camel Case.
     * Add Comments to make it easier to read the code.
     * Add whitespaces between code blocks.

```

### 2. XAML Coding Guidelines

#### Naming:

- Name elements with the ```x:Name``` attribute.
- Use Pascal Casing *(i.e. first char should be capitalized)*.
- Suffix XAML names with a type indication.
- Example:

```
x:Name = "EmailTextBox"
```

#### Code readability:

- Put one attribute per line.
- Put the first attribute on the element line.
- Put the ```x:Name``` or ```x:Key``` as the first attribute.
- Put the attached properties at the beginning of the element, eventually after the ```x:Name``` or ```x:Key```
- Order Attributes in the following order:

```

1. x:Key and x:Name

2. Attached properties
   - Grid.Row
   - Grid.Column

3. Positionning
   - HorizontalAlignment
   - VerticalAlignment
   - Margin
   - Padding
   - Stretch
   - Canvas.
   - Grid.

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

6. Visual
   - Background
   - Fill
   - Foreground
   - Borderbrush
   - Borderthickness
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

### 3. UI Guidelines

- Use **Grid** layout instead of StackPanel when possible.
- Add **15px** spacing between rows and columns.
- Add **25px** whitespace border in cards and windows.

### 4. Typography Guidelines

- Use **Product Sans** Font for buttons and headings.
- Else, use **Roboto** Font.
