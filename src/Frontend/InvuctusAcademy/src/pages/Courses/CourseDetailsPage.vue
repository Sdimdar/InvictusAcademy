<template>
  <q-page-container class="column" style="height: 1500px; padding-bottom: 0px;">

      <div class="col">
        <div class="row" style="margin-left: 20px;">
          <div class="column" style="width: 65%;">
            <div style="font-size: 48px; font-weight: 700; color: #000000;">
              Онлайн курс
            </div>
            <div style="font-size: 48px; font-weight: 700; color: #000000;">
              “ {{ course.name }}”
            </div>
            <div style="font-size: 16px; font-weight: 300; color: #000000; margin-top: 15px;">
               {{ course.description }}
            </div>
          </div>

          <div class="column" style="width: 35%;">
              <img class="preview" src="img/course_info.svg">
          </div>
        </div>
      </div>

      <div class="col"> Мой прогресс </div>
      <div class="col">
        Идеально подойдут вам
          <div class="row">

          </div>
      </div>
      <div class="col"> Недавно просмотренные </div>
      <div class="col-2"> Читайте также </div>
  </q-page-container>

</template>

<script>
import { ref } from "vue";
import { getCourseById } from "boot/axios";

export default {
  data() {
    return {
      id: Number(this.$route.query.id),
      course:"",

    };
  },
  mounted() {
    this.getCourseData()
  },
  methods: {
    async getCourseData() {
      const response = await getCourseById(this.id);
      console.log(response.data)
      this.course = response.data.value
    },
  },
};
</script>

<style>
.preview{
  height: 300px;
  width: 260px;
  padding: 10px;
}
</style>

