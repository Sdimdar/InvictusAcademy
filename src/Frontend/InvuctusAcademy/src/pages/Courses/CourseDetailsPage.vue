<template>
  <q-page-container class="column" style="padding-bottom: 30px;">

      <div class="col-3">
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
              <q-btn no-caps color="accent" class="start-btn" label="Начать обучение" @click="addPayment"/>
            </div>
          </div>

          <div class="column" style="width: 35%;">
              <img class="preview" src="img/course_info.svg">
          </div>
        </div>
      </div>

      <div class="col-1" style="margin-left: 20px; margin-top: 50px;">
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
                    Гарантия <br /> трудоустройства
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
                    Востребованный <br /> сертификат
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p > По окончанию курса <br /> вы получите сертификат </p>
                </div>
              </div>
              </div>
            </div>
        </div>

      <div class="col-3">
          <div class="row">
            <div class="column" style="width: 50%;">
              <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 10px;">
                {{ course.secondName }}
            </div>
            <div  style="font-size: 16px; font-weight: 300; color: #000000; margin: 20px 10px 10px 10px;" >
              {{ course.secondDescription }}
            </div>
            <div >
              <q-btn no-caps outline color="accent" class="start-btn" label="Начать обучение" @click="addPayment"
              style="margin: 20px 10px 10px 10px;" />
            </div>
            </div>
            <div class="column" style="width: 50%;">
              <div style="padding: 20px;">
                <iframe width="560" height="315" src="https://www.youtube.com/embed/GNgEW-Qh4XU" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
              </div>
            </div>
          </div>
      </div>

      <div class="col-3" style="margin-top: 50px;">
        <div class="row">
          <div class="column">
              <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 0px;">
                Кому этот курс подойдёт?
            </div>
            <div  style="font-size: 16px; font-weight: 300; color: #000000; margin: 20px 10px 10px 0px;" >
              Всем! Курс предназначен как для новичков, так и для профессионалов!<br />
              Вам подойдёт этот курс, если вы:
            </div>
            <div class="row" style="margin: 20px 10px 10px 0px;">
             <div class="col-3" v-for="(point, index) in course.coursePoints" :key="`points-${index}`">
                <div class="column">
                  <div class="col" style="font-size: 22px; font-weight: 600;">
                    {{ point.pointImageLink }}
                  </div>
                  <div class="col" style="font-size: 16px; font-weight: 300;">
                    <p> {{ point.point }}</p>
                </div>
              </div>
              </div>
            </div>
            </div>
            </div>
            <div class="btn-center">
              <q-btn no-caps color="accent" class="start-btn" label="Начать обучение"  @click="addPayment" />
            </div>
      </div>

      <div class="col-2" style="margin-top: 30px;">
        <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 10px;">
            А что внутри курса?
        </div>
        <div v-for="(oneModule, index) in courseModules" :key="`courseModules-${index}`">
          <q-card :class="$attrs.class" class="my-card" flat bordered @click="showDescription">
            <q-card-section horizontal class="flex justify-between">
              <q-card-section class="q-pt-xs">
                    <div  dense class="text-h5 q-mt-sm q-mb-xs">
                      Модуль № {{oneModule.id}} "{{ oneModule.title }}"
                    </div>
                    <div class="text-caption text-grey" v-show="isDescription">
                        {{ oneModule.shortDescription }}
                    </div>
                </q-card-section>
            </q-card-section>
        </q-card>
        </div>
      </div>

</q-page-container>

</template>

<script>
import { ref } from "vue";
import { getCourseById, getShortModulesInfo, addToPayments } from "boot/axios";
import notify from "boot/notifyes";

export default {
  data() {
    return {
      id: this.$route.query.id,
      course:"",
      courseModules: [],
      isDescription: false
    };
  },
  setup() {
    return {
      tab: ref("current"),
      splitterModel: ref(20),
    };
  },
  mounted() {
    this.getCourseData()
  },
  methods: {
    async getCourseData() {
      const response = await getCourseById(this.id);
      this.course = response.data.value
      console.log(this.course)
      this.getModuleData()
    },
    async getModuleData() {
      const courseId = this.id;
      try {
        const response = await getShortModulesInfo(courseId);
        if (response.data.isSuccess) {
          this.courseModules = response.data.value;
          console.log(this.courseModules)
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    showDescription() {
      this.isDescription = !this.isDescription;
    },
    async addPayment() {
      const response = await addToPayments(this.id);
      if(response.data.isSuccess){
        console.log(response.data.value)
        notify.showSucsessNotify("Запрос на покупку отправлен менеджер свяжется  вами!");
      }
      else{
        notify.showSucsessNotify("Что-то пошло не так. Попробуйте еще раз позже");
      }
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
  border-radius: 10px;
}

.btn-center{
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-top: 30px;
}

</style>

