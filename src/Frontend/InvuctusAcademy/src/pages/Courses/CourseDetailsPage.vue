<template>
  <q-page-container class="column" style="height: 1800px; padding-bottom: 0px;">

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
            <div class="row">
              <div class="col-5">
                <div class="column">
                  <div class="col-2 small-text">
                    Дата начала:
                  </div>
                  <div class="col big-text">
                    <p> В любое время </p>
                </div>
              </div>
              </div>
              <div class="col-3">
                <div class="column">
                  <div class="col-2 small-text">
                    Формат:
                  </div>
                  <div class="col big-text">
                    <p> Онлайн </p>
                </div>
              </div>
              </div>
              <div class="col-4">
                <div class="column">
                  <div class="col-2 small-text">
                    Количество модулей:
                  </div>
                  <div class="col big-text">
                    <p> {{courseModules.length}}</p>
                </div>
              </div>
              </div>
            </div>
            <div>
              <q-btn no-caps color="accent" class="start-btn" label="Начать обучение" />
            </div>
          </div>

          <div class="column" style="width: 35%;">
              <img class="preview" src="img/course_info.svg">
          </div>
        </div>
      </div>

      <div class="col" style="margin-left: 20px;">
        <div class="row">
              <div class="col-3">
                <div class="column">
                  <div class="col" style="font-size: 22px; font-weight: 600;">
                    Самая актуальная информация
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p> С учётом всех изменений <br /> на рынке </p>
                </div>
              </div>
              </div>
              <div class="col-3">
                <div class="column">
                  <div class="col" style="font-size: 22px; font-weight: 600;">
                    Гарантия трудоустройства
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p> И персональный ментор, <br />  который всегда на связи </p>
                </div>
              </div>
              </div>
              <div class="col-3">
                <div class="column">
                  <div class="col" style="font-size: 22px; font-weight: 600;">
                    Возможность <br />  учиться удалённо
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p>Из любой точки мира, <br />  нужен всего-лишь вай-фай</p>
                </div>
              </div>
              </div>
              <div class="col-3">
                <div class="column">
                  <div class="col" style="font-size: 22px; font-weight: 600;">
                    Востребованный сертификат
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p > По окончанию курса <br /> вы получите сертификат </p>
                </div>
              </div>
              </div>
            </div>
        </div>

      <div class="col-2">
        Идеально подойдут вам
          <div class="row">

          </div>
      </div>
      <div class="col-2"> Недавно просмотренные </div>
      <div class="col-2"> Читайте также </div>
  </q-page-container>

</template>

<script>
import { ref } from "vue";
import { getCourseById, getShortModulesInfo } from "boot/axios";

export default {
  data() {
    return {
      id: Number(this.$route.query.id),
      course:"",
      courseModules: []
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
      const modulesresponse = await getShortModulesInfo(this.id);
      console.log(modulesresponse.data)
      this.courseModules = modulesresponse.data.value
    },
  },
};
</script>

<style>
.preview{
  height: 320px;
  width: 260px;
  padding: 10px;
}
.small-text{
  font-size: 16px;
  font-weight: 300;
  color: #000000;
  margin-top: 20px;
}

.big-text{
  font-size: 24px;
  font-weight: 700;
  color: #000000;
  margin-top: 10px;
}
.start-btn{
  padding: 15px;
  box-shadow: 0px 4px 22px rgba(177, 20, 20, 0.12);
  border-radius: 10px;
}

</style>

