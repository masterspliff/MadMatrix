# Code Conventions

## Documentation

1. Create small, focused notes near relevant code blocks to explain:
   - The purpose of complex logic
   - Any non-obvious design decisions
   - Important implementation details
   - Known limitations or edge cases

Example:
```csharp
// Using StringBuilder here for better performance with large strings
var builder = new StringBuilder();
```

These inline notes should be:
- Concise and to the point
- Located as close as possible to the relevant code
- Written in clear, simple language

## CSS Styling

1. Use [Bootstrap](https://getbootstrap.com/) as the primary CSS framework.
2. Follow the Bootstrap [documentation](https://getbootstrap.com/docs/5.0/getting-started/introduction/) for usage guidelines.
3. Keep custom CSS to a minimum. Try to use Bootstrap classes and utilities whenever possible.
4. For page-specific styling:
   - Create a scoped CSS file for each page that needs custom styles
   - Name the file to match the page (e.g., `MyPage.razor.css` for `MyPage.razor`)
   - Place the CSS file in the same directory as the page
   - Use scoped CSS to prevent style leaks between components
5. Keep global CSS (`wwwroot/css/app.css`) minimal and only for truly application-wide styles
6. When custom CSS is necessary, ensure it is well-documented and follows best practices for readability and maintainability.

Example:
```
Pages/
  ├── MyPage.razor
  └── MyPage.razor.css  // Scoped CSS for MyPage
```

