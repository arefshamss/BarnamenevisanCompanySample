let theme = localStorage.getItem('color-theme') ?? "dark";
if (theme === "dark")
    document.documentElement.classList.add('dark');
else 
    document.documentElement.classList.remove('dark');