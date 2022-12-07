<template>
  <q-page-container v-if="!logined">
    <div>

    </div>

  </q-page-container>

  <q-page-container v-else class="column" style="padding-bottom: 0px; ">
      <div class="col">
        <div class="video-container">
        <video autoplay muted loop style="width: 1140px;">
            <source src="video/invictus_video.mp4" type="video/mp4" />
        </video>
        </div>
      </div>

      <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
        Мои курсы
        <div class="row">
          <course-card class="list-card" v-for="course in currentCourses" :data="course" />
        </div>
      </div>

      <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
        Идеально подойдут вам
          <div class="row">
            <div>
              <q-btn dense round unelevated color="accent" icon="chevron_left"
                  @click="prevCourses" v-show="currentLenght" />
            </div>
            <course-card class="list-card" v-for="course in showCourses" :data="course" @wished="getCoursesData"/>
            <div>
              <q-btn dense round unelevated color="accent" icon="chevron_right"
                  @click="nextCourses()"  :disable="(newCourses.length < current)"/>
            </div>
            </div>
      </div>
      <div class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
        Избранное
        <div class="row">
          <course-card class="list-card" v-for="course in wishedCourses" :data="course" @wished="getCoursesData"/>
        </div>
      </div>
      <div class="col-2" style="font-size: 32px; font-weight: 700; color: #000000;">
        Читайте также
        <div class="row">

        </div>
      </div>
  </q-page-container>

</template>

<script>
import { defineComponent} from 'vue'
import {
  getCurrentCourses,
  getCompletedCourses,
  getWishedCourses,
  getNewCourses,
} from "boot/axios";

import RequestButton from 'components/RequestButton.vue'
import CourseCard from "components/Courses/CourseCard.vue";

export default defineComponent({
  name: 'IndexPage',
  components: {
    RequestButton,
    CourseCard,
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
      wishedCourses:[],
      newCourses: [],
      showCourses : [],
      current: 4,
      currentLenght: false
    };
  },
  mounted() {
    this.getCoursesData();
  },
  methods: {
    async getCoursesData() {
      try {
        const response = await getNewCourses();
        if (response.data.isSuccess) {
          this.newCourses = response.data.value.courses;
          console.log(this.newCourses);
          this.showCourses =this.newCourses.slice(0, 4)
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
    	if( this.current < this.newCourses.length)
        this.showCourses =this.newCourses.slice(this.current, this.current+4)
      	this.current = this.current + 4
        this.currentLenght = true
        console.log(this.current)
    },
    prevCourses() {
    	if(this.current > 4)
        this.current = this.current - 4
        this.showCourses =this.newCourses.slice(this.current-4, this.current)
        if(this.current === 4){
          this.currentLenght = false
        }
        console.log(this.current)
    }
  },
})
</script>

<style>
.video-container {
    height: 200px;
    width:  200px;
    margin-top: -50px;
    position: relative;
}

.video-container video {
  width: 100%;
  height: 100%;
  position: absolute;
  object-fit: cover;
  z-index: 0;
}


</style>
