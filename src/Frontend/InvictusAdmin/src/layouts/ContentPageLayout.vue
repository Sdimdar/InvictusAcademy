<template>
<div>
    <q-splitter
      v-model="splitterModel"
      unit="px"
      style="height: 400px"
    >
      <template v-slot:before>
        <div class="q-pa-md">
          <div class="text-h6 q-mb-md">Учебные материалы</div>
          <createModule-button  class="nav-button" />
        </div>
      </template>

      <template v-slot:after>
          <div class="q-pa-md">
            <div class="q-gutter-y-md" style="max-width: 1000px">
        <q-tabs
          v-model="tab"
          align="left"
          inline-label
          class="bg-primary text-white shadow-2"
        >
          <q-route-tab to="/admin-panel/showAllModules" icon="star" label="Все модули" />
          <q-route-tab to="/admin-panel/showAllArticules" icon="star" label="Раздаточный материал" />
        </q-tabs>
    </div>

    <q-page-container>
        <q-page padding>
          <router-view />
        </q-page>
      </q-page-container>

        </div>
      </template>

    </q-splitter>
  </div>


</template>

<script>
import {  } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import CreateModuleButton from 'components/CreateModuleButton.vue'

export default {
  name: 'Content',
  components:{
   CreateModuleButton
  },
  setup () {
    return {
      splitterModel: ref(250)
    }
  },
  data(){
    return{

    }
  },
  methods:{
}
}
</script>
