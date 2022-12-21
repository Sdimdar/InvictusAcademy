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
                  <p> {{ courseModules.length }}</p>
                </div>
              </div>
            </div>
          </div>

          <div>
            <q-btn v-if="isPurchased" :href="'/course/' + course.id" no-caps color="accent" class="start-btn"
              label="Перейти к обучению" />
            <course-payment-button v-else :title="course.name" :id="id" :cost="course.cost" />
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
              <p> И персональный ментор, <br /> который всегда на связи </p>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="column">``
            <div class="col" style="font-size: 22px; font-weight: 600;">
              Возможность <br /> учиться удалённо
            </div>
            <div class="col" style="font-size: 16px; font-weight: 300;">
              <p>Из любой точки мира, <br /> нужен всего-лишь вай-фай</p>
            </div>
          </div>
        </div>
        <div class="col-3">
          <div class="column">
            <div class="col" style="font-size: 22px; font-weight: 600;">
              Востребованный <br /> сертификат
            </div>
            <div class="col" style="font-size: 16px; font-weight: 300;">
              <p> По окончанию курса <br /> вы получите сертификат </p>
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
          <div style="font-size: 16px; font-weight: 300; color: #000000; margin: 20px 10px 10px 10px;">
            {{ course.secondDescription }}
          </div>
          <div>
            <q-btn v-if="isPurchased" :href="'/course/' + course.id" no-caps color="accent" class="start-btn"
              label="Перейти к обучению" />
            <course-payment-button v-else :title="course.name" :id="id" :cost="course.cost" />
          </div>
        </div>
        <div class="column" style="width: 50%;">
          <div style="padding: 20px;">
            <video width="560" height="315" class="article-video" controls="controls" :src="course.videoLink"></video>
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
          <div style="font-size: 16px; font-weight: 300; color: #000000; margin: 20px 10px 10px 0px;">
            Всем! Курс предназначен как для новичков, так и для профессионалов!<br />
            Вам подойдёт этот курс, если вы:
          </div>
          <div class="row point-box">
            <div class="column point-style" v-for="(point, index) in course.coursePoints" :key="`points-${index}`">
              <div class="point-image-box">
                <img class="image-targeting" :src="point.pointImageLink" />
              </div>
              <div class="point-text" style="">
                <p> {{ point.point }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <q-btn v-if="isPurchased" :href="'/course/' + course.id" no-caps color="accent" class="start-btn"
          label="Перейти к обучению" />
        <course-payment-button v-else :title="course.name" :id="id" :cost="course.cost" />
      </div>
    </div>


    <div class="col-2" style="margin-top: 30px;">
      <div style="font-size: 32px; font-weight: 700; color: #000000; margin: 20px 10px 10px 10px;">
        А что внутри курса?
      </div>
      <div>
        <q-card style="margin-right:50px; padding:0 20px 30px">
          <q-card-section v-for="(item, index) in courseModules" :key="item.id" style="padding-bottom:0">
            <q-expansion-item expand-separator icon="perm_identity" style="border-bottom: 1px solid #E9E9E9;">
              <template v-slot:header>
                <q-item-section>
                  {{ `Модуль №${index + 1} - ''${item.title}''` }}
                </q-item-section>
              </template>
              <q-card>
                <q-card-section>
                  {{ item.shortDescription }}
                </q-card-section>
              </q-card>
            </q-expansion-item>
          </q-card-section>
        </q-card>
      </div>
    </div>

  </q-page-container>

</template>

<script>
import { ref } from "vue";
import { getCourseById, getShortModulesInfo, addToPayments, getCurrentCourses } from "boot/axios";
import notify from "boot/notifyes";
import CoursePaymentButton from "components/Courses/CoursePaymentButton.vue";

export default {
  components: {
    CoursePaymentButton
  },
  data() {
    return {
      id: this.$route.query.id,
      course: "",
      courseModules: [],
      isDescription: false,
      purchasedCourses: [],
      isPurchased: false
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
      this.getPurchasedCourses()
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
    async getPurchasedCourses() {
      try {
        const response = await getCurrentCourses();
        if (response.data.isSuccess) {
          this.purchasedCourses = response.data.value;
          console.log(this.purchasedCourses)
          let checkCourse = this.purchasedCourses.courses.find((element) => element.id === this.id)
          if (checkCourse) {
            this.isPurchased = true
          }
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
      if (response.data.isSuccess) {
        console.log(response.data.value)
        notify.showSucsessNotify("Запрос на покупку отправлен менеджер свяжется  вами!");
      }
      else {
        notify.showErrorNotify("Что-то пошло не так. Попробуйте еще раз позже");
      }
    },
  },
};
</script>

<style>
.preview {
  height: 320px;
  width: 300px;
  padding: 10px;
}

.small-text {
  font-size: 16px;
  font-weight: 300;
  color: #000000;
  margin-top: 20px;
}

.big-text {
  font-size: 24px;
  font-weight: 700;
  color: #000000;
  margin-top: 10px;
}

.start-btn {
  padding: 15px;
  border-radius: 10px;
}

.btn-center {
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-top: 30px;
}

.point-box {
  margin-top: 30px;
  display: flex;
  gap: 10px;
}

.point-style {
  padding: 5px;
  width: 200px;
  text-align: center;
}

.point-image-box {
  margin-bottom: 10px;
  font-size: 22px;
  font-weight: 600;
  margin-bottom: 10px;
}

.image-targeting {
  width: 90%;
}

.point-text {
  font-size: 16px;
  font-weight: 300;
}
</style>
