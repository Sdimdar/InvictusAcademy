<template>
    <q-layout view="lHh Lpr lFf">
        <left-bar />
        <div class="my-courses">
            <h4 style="text-align: center">Модули курса</h4>

            <q-splitter v-model="splitterModel" style="height: 750px, margin-left: 100px">
                <template v-slot:before>
                    <q-tabs v-model="tab" vertical class="text-teal">
                        <q-tab name="current" icon="mail" label="Текущие" />
                    </q-tabs>
                </template>

                <template v-slot:after>
                    <q-tab-panels v-model="tab" animated swipeable vertical transition-prev="jump-up"
                        transition-next="jump-up">
                        <q-tab-panel name="current">
                            <div class="text-h4 q-mb-md">Текущие</div>
                            <course-card class="list-card" v-for="course in currentCourses" :data="course" />
                        </q-tab-panel>
                    </q-tab-panels>
                </template>
            </q-splitter>
        </div>
    </q-layout>
</template>
  
<script>
import { defineComponent } from "vue";
import { fetchModuleById} from "boot/axios";
import notify from "boot/notifyes";
import ModuleCard from "components/ModuleCard.vue";
import LeftBar from "components/LeftBar.vue";

export default defineComponent({
  name: "CourseModulesPage",
  components:{
    ModuleCard
  },
  data() {
      return {
        id: this.$route.params.id,
        title:"",
        shortDescription: "",
        articles: []
      };
    },
  methods: {
    async getModuleData(){
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.value.shortDescription
      this.title = response.data.value.value.title
      this.articles = response.data.value.value.articles
      console.log(this.articles)
    },
    
  },
  beforeMount(){
    this.getModuleData()
 },
});
</script>

  
<style scoped>
.my-courses {
    margin-left: 20%;
}
</style>