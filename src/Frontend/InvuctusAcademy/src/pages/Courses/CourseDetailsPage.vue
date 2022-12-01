<template>
  <q-page-container class="column" style="height: 2000px; padding-bottom: 0px;">

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
              <q-btn no-caps color="accent" class="start-btn" label="Начать обучение" />
            </div>
          </div>

          <div class="column" style="width: 35%;">
              <img class="preview" src="img/course_info.svg">
          </div>
        </div>
      </div>

      <div class="col-1" style="margin-left: 20px;">
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
                    Востребованный сертификат
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
              <q-btn no-caps outline color="accent" class="start-btn" label="Начать обучение"
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

      <div class="col-3">
        <div class="row">
          <div class="column">
              <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 10px;">
                Кому этот курс подойдёт?
            </div>
            <div  style="font-size: 16px; font-weight: 300; color: #000000; margin: 20px 10px 10px 10px;" >
              Всем! Курс предназначен как для новичков, так и для профессионалов!<br />
              Вам подойдёт этот курс, если вы:
            </div>
            <div class="row" style="margin: 20px 10px 10px 10px;">
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
            <div class="btn-center">
              <q-btn no-caps color="accent" class="start-btn" label="Начать обучение" />
            </div>
            </div>
            </div>
      </div>
      <div class="col-2">
        <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 10px;">
            А что внутри курса?
        </div>
        <div class="q-pa-md">
    <q-table
      :title="title"
      :rows="rows"
      :columns="columns"
      row-key="name"
    >
      <template v-slot:header="props">
        <q-tr :props="props">
          <q-th auto-width />
          <q-th
            v-for="col in props.cols"
            :key="col.name"
            :props="props"
          >
            {{ col.label }}
          </q-th>
        </q-tr>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td auto-width>
            <q-btn size="sm" color="accent" round dense @click="props.expand = !props.expand" :icon="props.expand ? 'remove' : 'add'" />
          </q-td>
          <q-td
            v-for="col in props.cols"
            :key="col.name"
            :props="props"
          >
            {{ col.value }}
          </q-td>
        </q-tr>
        <q-tr v-show="props.expand" :props="props">
          <q-td colspan="100%">
            <div class="text-left">This is expand slot for row above: {{ props.row.name }}.</div>
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </div>
  </div>
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
  border-radius: 10px;
}

.btn-center{
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-top: 30px;
}

</style>

