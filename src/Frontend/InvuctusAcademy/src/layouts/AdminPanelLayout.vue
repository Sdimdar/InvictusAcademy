<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item to="/admin-panel" >
          <img class="logo-img" src="img/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <q-btn to="/admin-panel/showAllUser" class="nav-button" label="Список пользователей"/>
        <q-btn to="/admin-panel/requests" class="nav-button" label="Список заявок"/>
      </q-toolbar>
    </q-header>
    <q-page-container>
        <router-view />
    </q-page-container>
  </q-layout>
</template>


<script>

export default {
  name: 'AdminPanelLayout',
  
}
</script>

<style>
  .logo-img{
    width: 200px;
    object-fit: contain;
  }
  .nav-button{
    font-size: 11px;
    border-radius: 0;
    align-self: stretch;
  }
</style>