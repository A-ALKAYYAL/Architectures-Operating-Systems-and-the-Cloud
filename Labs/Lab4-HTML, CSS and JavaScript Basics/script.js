const toggleBtn = document.querySelector("#darkModeToggle");

const enableDarkMode = () => {
    document.body.classList.add("dark-mode");
    localStorage.setItem("darkMode", "enabled");
};

const disableDarkMode = () => {
    document.body.classList.remove("dark-mode");
    localStorage.setItem("darkMode", "disabled");
};

const darkModeSetting = localStorage.getItem("darkMode");

if (darkModeSetting === "enabled") {
    enableDarkMode();
}

toggleBtn.addEventListener("click", () => {
    const currentSetting = localStorage.getItem("darkMode");
    
    if (currentSetting !== "enabled") {
        enableDarkMode();
    } else {
        disableDarkMode();
    }
});