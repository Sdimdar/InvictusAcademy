<template>
<div>
    <q-splitter
      v-model="splitterModel"
      unit="px"
      style="height: 800px"
    >
      <template v-slot:before>
        <div class="q-pa-md">
          <div class="text-h6 q-mb-md">Учебные материалы</div>
          <createModule-button
          class="nav-button"
          @createModule="update"/>

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
          <q-route-tab icon="book" label="Все модули" />
        </q-tabs>
    </div>

    <q-page-container>
          <modulesInfo-page />
      </q-page-container>

        </div>
      </template>
    </q-splitter>
  </div>



</template>

<script>
import {  } from "boot/axios";
import { ref } from 'vue';
import CreateModuleButton from 'src/components/Module/CreateModuleButton.vue'
import ModulesInfoPage from 'src/components/Module/ModulesInfoPage.vue'

export default {
  name: 'Content',
  components:{
   CreateModuleButton,
   ModulesInfoPage
  },
  setup () {
    return {
      splitterModel: ref(250),
      slide: ref('style')
    }
  },
  data(){
    return{

    }
  },
  methods:{
    update(){
      window. location. reload()
    }
}
}
</script>
