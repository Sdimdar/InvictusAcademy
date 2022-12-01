<template>
  <div>
    <q-splitter v-model="splitterModel" unit="px" style="height: 800px">
      <template v-slot:before>
        <div class="q-pa-md">
          <div class="text-h6 q-mb-md">{{ title }}</div>
          <div class="text-h8 q-mb-md">{{ shortDescription }}</div>
          <q-btn to="/admin-panel/content" label="Назад" />

        </div>
      </template>

      <template v-slot:after>
        <div class="q-pa-md">
          <div class="q-gutter-y-md">
            <q-tabs align="left" inline-label class="bg-primary text-white shadow-2">
              <q-route-tab icon="book" label="Статьи модуля" />

            </q-tabs>
          </div>

          <q-page-container>

            <createArticle :title="title" :id="id" :articles="articles" @addArticle="getModuleData" />
            <div>
              <table class="styled-table">
                  <tbody>
                      <tr v-for="article in articles" :key="article.order">
                          <td>{{article.order}}</td>
                          <td>{{article.title}}</td>
                          <q-btn label="Детали" @click="openDetails(id, article.order)" />
                      </tr>
                  </tbody>
              </table>
    </div>

          </q-page-container>

        </div>
      </template>
    </q-splitter>
  </div>

</template>

<script>
import { defineComponent, ref } from "vue";
import { updateModule, fetchModuleById } from "boot/axios";
import notify from "boot/notifyes";
import CreateArticle from 'src/components/Module/CreateArticleButton.vue'


export default defineComponent({
  name: "detailsPage",
  components: {
    CreateArticle
  },
  setup() {
    return {
      splitterModel: ref(250),
      slide: ref('style')
    }
  },
  data() {
    return {
      id: this.$route.params.id,
      title: "",
      shortDescription: "",
      articles: []
    };
  },
  methods: {
    async getModuleData() {
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.shortDescription
      this.title = response.data.value.title
      this.articles = response.data.value.articles
      console.log(this.articles)
    },
    async onSubmit() {
      try {
        let payload = {
          Id: this.id,
          Title: this.title,
          ShortDescription: this.shortDescription
        }
        const response = await updateModule(payload);
        console.log(response)

        if (response.data.value.isSuccess) {
          this.updateDialog = false
          notify.showSucsessNotify("Изменения сохранены");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.title = "";
      this.shortDescription = "";
      this.updateDialog = false;
      this.errorMessage = "";
    },
    openDetails(id, order){
    this.$router.push({path:'/admin-panel/article', query:{id: id, order: order }})
  }
  },
  beforeMount() {
    this.getModuleData()
  },
});
</script>

<style>
.styled-table{
    border-collapse: collapse;
    margin: 50px 0;
    min-width: 800px;
}
.styled-table tbody tr {
    border-bottom: 1px solid #dddddd;
    padding: 10px;
}

</style>
