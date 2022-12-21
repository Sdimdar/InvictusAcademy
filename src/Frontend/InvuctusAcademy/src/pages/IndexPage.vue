<template>
  <q-page-container v-if="logined" class="column" style="padding-bottom: 0px; ">

    <div class="col">
      <div class="video-container">
        <video autoplay muted loop >
          <source src="https://invictus.object.pscloud.io/video/I AM INVICTUS.mp4" type="video/mp4" />
        </video>
      </div>
    </div>

    <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;" v-if="currentCourses.length != 0" >
      Мои курсы
      <div class="row">
        <course-card class="list-card" v-for="course in currentCourses" :data="course" />
      </div>
    </div>

    <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;" v-if="showCourses.length != 0">
      Идеально подойдут вам
      <div class="row">
        <div>
          <q-btn dense round unelevated color="accent" icon="chevron_left" @click="prevCourses"
            v-show="currentLenght" />
        </div>
        <course-card class="list-card" v-for="course in showCourses" :data="course" @wished="getCoursesData" />
        <div>
          <q-btn dense round unelevated color="accent" icon="chevron_right" @click="nextCourses()"
            :disable="(newCourses.length < current)" />
        </div>
      </div>
    </div>
    <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;" v-if="wishedCourses.length != 0">
      Избранное
      <div class="row">
        <course-card class="list-card" v-for="course in wishedCourses" :data="course" @wished="getCoursesData" />
      </div>
    </div>
    <div class="col-2" style="font-size: 32px; font-weight: 700; color: #000000;" v-if="allFreeAticles.length != 0">
      Читайте также
      <div class="row">
        <free-article-card :freeArticle="freeArticle" v-for="freeArticle in allFreeAticles"/>
      </div>
    </div>
    <div class="col-2" style="font-size: 32px; font-weight: 700; color: #000000;" v-if="allStreamingRooms.length != 0">
        Скорые трансляции
        <div class="row">
          <stream-card :stream="stream" v-for="stream in allStreamingRooms"/>
        </div>
    </div>
  </q-page-container>

</template>

<script>
import { useQuasar } from 'quasar'
import { defineComponent } from 'vue'
import {
  getCurrentCourses,
  getCompletedCourses,
  getWishedCourses,
  getNewCourses,
  fetchAllFreeArticles,
  getAllStreamingRooms
} from "boot/axios";
import RequestButton from 'components/RequestButton.vue'
import CourseCard from "components/Courses/CourseCard.vue";
import FreeArticleCard from "src/components/FreeArticle/FreeArticleCard.vue";
import StreamCard from "src/components/Jitsi/StreamCard.vue"

export default defineComponent({
  name: 'IndexPage',
  components: {
    RequestButton,
    CourseCard,
    FreeArticleCard,
    StreamCard
  },
  props: {
    logined: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      currentCourses: [],
      completedCourses: [],
      wishedCourses: [],
      newCourses: [],
      showCourses: [],
      current: 4,
      currentLenght: false,
      allFreeAticles: [],
      allStreamingRooms : [],
      quasar: useQuasar()
    };
  },
  async mounted() {
    this.quasar.loading.show()
    await this.getCoursesData()
    await this.getFreeArticles()
    await this.getAllStreamingRooms()
    this.quasar.loading.hide()
  },
  methods: {
    async getAllStreamingRooms() {
      try {
        const response = await getAllStreamingRooms(1, 5);
        if (response.data.isSuccess) {
          this.allStreamingRooms = response.data.value.streamingRooms;
        } else {
          response.data.errors.forEach(element => {
            console.log(element)
          });
        }
      } catch (e) {
        console.log(e.message)
      }
    },
    async getCoursesData() {
      try {
        const response = await getNewCourses();
        if (response.data.isSuccess) {
          this.newCourses = response.data.value.courses;
          console.log(this.newCourses);
          this.showCourses = this.newCourses.slice(0, 4)
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getCompletedCourses();
        if (response.data.isSuccess) {
          this.completedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getWishedCourses();
        if (response.data.isSuccess) {
          this.wishedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getCurrentCourses();
        if (response.data.isSuccess) {
          this.currentCourses = response.data.value.courses;
          console.log(response.data.value.courses)
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    nextCourses() {
      if (this.current < this.newCourses.length)
        this.showCourses = this.newCourses.slice(this.current, this.current + 4)
      this.current = this.current + 4
      this.currentLenght = true
      console.log(this.current)
    },
    prevCourses() {
      if (this.current > 4)
        this.current = this.current - 4
      this.showCourses = this.newCourses.slice(this.current - 4, this.current)
      if (this.current === 4) {
        this.currentLenght = false
      }
      console.log(this.current)
    },
    async getFreeArticles() {
      try {
        const response = await fetchAllFreeArticles(1, 5);
        if (response.data.isSuccess) {
          this.allFreeAticles = response.data.value.freeArticles;
        } else {
          response.data.errors.forEach(element => {
            console.log(element);
          });
        }
      } catch (e) {
        console.log(e.message);
      }
    }
  },
})
</script>

<style>
.video-container {
  height: 300px;
  width: 95%;
  position: relative;
  margin: -50px auto 50px;
}

.video-container video {
  width: 100%;
  height: 100%;
  position: absolute;
  object-fit: cover;
  z-index: 0;
}
</style>
