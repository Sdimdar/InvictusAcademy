<template>
  <q-page-container>
    <h4 style="text-align: center">Модули курса</h4>
    <q-splitter>
      <template v-slot:after>
        <q-tab-panels v-model="tab" animated swipeable vertical transition-prev="jump-up" transition-next="jump-up">
          <q-tab-panel name="current">
            <module-card class="list-card" v-for="onemodule in getShortModulesInfo" :data="onemodule" />
          </q-tab-panel>
        </q-tab-panels>
      </template>
    </q-splitter>
  </q-page-container>
</template>

<script>
import { ref } from "vue";
import { getShortModulesInfo } from "boot/axios";
import ModuleCard from "components/Modules/ModuleCard.vue";

export default ({
  components: { moduleCard },
  props: {
    logined: {
      Type: Boolean,
    },
  },
  setup() {
    return {
      tab: ref("current"),
      splitterModel: ref(20),
    };
  },
  mounted() {
    if (!this.logined) {
      this.$router.push({ name: "homepage" });
    }
    this.getModuleData();
  },
  data() {
    return {
      id: this.$route.params.id,
      getShortModulesInfo: [],

    };
  },
  methods: {
    async getModuleData() {
      const courseId = this.id;
      try {
        const response = await getShortModulesInfo(courseId);
        if (response.data.isSuccess) {
          this.getShortModulesInfo = response.data.value;
        }
      } catch (error) {
        console.log(error.message);
      }
    },

  },

  // beforeMount() {
  //   this.getModuleData()
  // },
});
</script>


<style scoped>
.my-modules {
  margin-left: 20%;
}
</style>
