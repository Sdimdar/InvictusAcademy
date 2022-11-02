<template>
  <q-drawer v-model="leftDrawerOpen" show-if-above class="bg-grey-8">
    <q-list dark>
      <q-item-label header>Essential Links</q-item-label>
      <q-item clickable rel="noopener" to="/user">
        <q-item-section avatar>
          <q-icon name="code" />
        </q-item-section>
        <q-item-section>
          <q-item-label>Мои данные</q-item-label>
        </q-item-section>
      </q-item>
      <q-item clickable rel="noopener" to="/user/courses">
        <q-item-section avatar>
          <q-icon name="school" />
        </q-item-section>
        <q-item-section>
          <q-item-label>Мои курсы</q-item-label>
        </q-item-section>
      </q-item>
      <q-item clickable rel="noopener" to="/user/notbuyedcourses">
        <q-item-section avatar>
          <q-icon name="today" />
        </q-item-section>
        <q-item-section>
          <q-item-label>Все курсы</q-item-label>
        </q-item-section>
      </q-item>
    </q-list>
  </q-drawer>
  <q-page-container> </q-page-container>
</template>

<script>
import { defineComponent } from "vue";
import { fetchUserData } from "boot/axios";
import { fetchLoginedUserData } from "boot/axios";

export default defineComponent({
  name: "LeftBar",
  components: {},
  data() {},
  methods: {},
  mounted() {},
  updated() {},
});
</script>

<style scoped>
</style>
