const sideBar_items = document.querySelectorAll('.sidebar__menu--item');
const sideBar = document.querySelector('.sidebar');

const rootTheme = document.querySelector(':root');

const sideBarWidth_extend = getComputedStyle(document.documentElement).getPropertyValue('--width-sidebar-extend');
const sideBarWidth_collapse = getComputedStyle(document.documentElement).getPropertyValue('--width-sidebar-collapse');

sideBar_items.forEach(item => {
    // Hover vào item thì mở rộng sidebar
    item.addEventListener('mouseover', () => {
        rootTheme.style.setProperty('--width-sidebar', sideBarWidth_extend);
        sideBar.classList.remove('collapse');
    });
    // Mouse out thì thu nhỏ sidebar
    item.addEventListener('mouseout', () => {
        rootTheme.style.setProperty('--width-sidebar', sideBarWidth_collapse);
        if (!sideBar.classList.contains('collapse')) sideBar.classList.toggle('collapse');
    });
})

// const sideBar_collapseButton = document.querySelector(".sidebar__footer--collapse-button");
// sideBar_collapseButton.addEventListener('click', () => {
//     rootTheme.style.setProperty('--width-sidebar', sideBarWidth_collapse);
//     if (!sideBar.classList.contains('collapse')) sideBar.classList.toggle('collapse');
// });