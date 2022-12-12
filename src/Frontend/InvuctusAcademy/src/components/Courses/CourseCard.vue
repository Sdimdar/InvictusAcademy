<template>
  <div>
    <q-card class="my-card">
      <q-img src="https://cdn.quasar.dev/img/parallax2.jpg">
        <div class="absolute-bottom">
          <div class="text-h6">{{ data.name }}</div>
        </div>
      </q-img>

      <q-card-actions>
        <q-btn v-if="data.purchased" :href="'/course/' + data.id" flat color="primary">
          Перейти к курсу
        </q-btn>
        <q-btn v-if="!data.purchased" @click="openPage(data.id)">
          Детали
        </q-btn>
        <div style="margin-left: 10px;">
          <q-btn v-if="!data.purchased" flat round color="accent" :icon="isWished ? 'favorite' : 'favorite_border'"
            @click="addOrRemoveWished(data.id)" />
        </div>
      </q-card-actions>
    </q-card>

  </div>
</template>

<script>
import { addToWished, getWishedCourses, removeFromWished } from "boot/axios";
import notify from "boot/notifyes";

export default {
  props: {
    data: Object
  },
  data() {
    return {
      isWished: false
    };
  },
  mounted() {
    this.getWishedData()
  },
  methods: {
    openPage(id) {
      console.log(id);
      this.$router.push({ path: '/user/courseDetails', query: { id: id } })
    },
    async addOrRemoveWished(id) {
      console.log(this.isWished)
      if (this.isWished === false) {
        try {
          console.log(this.data)
          let courseId = Number(id)
          let payload = {
            CourseId: courseId
          }
          const response = await addToWished(payload);
          if (response.data.isSuccess) {
            notify.showSucsessNotify("Курс добавлен в избранное!");
            this.isWished = true
            this.$emit("wished");
          }
        } catch (error) {
          notify.showErrorNotify(e.message);
        }
      }
      else {
        try {
          let courseId = Number(id)
          let payload = {
            CourseId: courseId
          }
          const response = await removeFromWished(payload);
          if (response.data.isSuccess) {
            notify.showSucsessNotify("Курс удален из избранного!");
            this.isWished = false
            this.$emit("wished");
          }
        } catch (error) {
          notify.showErrorNotify(e.message);
        }
      }

    },
    async getWishedData() {
      try {
        const response = await getWishedCourses();
        if (response.data.isSuccess) {
          let wishedCourses = response.data.value.courses;
          let course = wishedCourses.find(c => c.id === this.data.id)
          if (course) {
            this.isWished = true
          }
        }
      } catch (error) {
        console.log(error.message);
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.course-img {
  height: 100px;
  width: 500px;
}

.my-card {
  margin: 10px;
  width: 220px;
  border-radius: 12px;
}
</style>
