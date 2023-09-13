<template>
    <li class="sidebar-item center">
        <router-link :to="path" class="sidebar-item__link center" href="">
            <m-icon :icon="icon"></m-icon>
            <div class="sidebar-item__title ml-8">{{ title }}</div>
        </router-link>
    </li>
</template>

<script>
export default {
    name: "MISASidebarItem",
    props: {
        icon: {
            type: String,
        },
        title: {
            type: String,
        },
        path: {
            type: String,
            default:"/home"
        }
    }
}
</script>

<style></style>