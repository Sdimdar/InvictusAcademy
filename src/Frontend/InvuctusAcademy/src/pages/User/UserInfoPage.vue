<template>
  <q-page-container>
    <div style="font-size: 32px; font-weight: 700; color: #000000; margin-bottom: 20px;">
      Личный кабинет
    </div>
    <div class="row" style="margin-bottom: 20px;">
      <div class="column" style="width: 45%;">
        <q-card class="my-card" flat bordered>
          <q-card-section class="text-center">
            <q-avatar size="140px" style="margin-bottom: 15px;">
              <img src="img/icons/avatar.svg">
            </q-avatar>
            <div style="font-size: 22px; font-weight: 600; color: #000000;"> {{ firstName }} {{ lastName }} </div>
            <div style="font-size: 16px; font-weight: 300; color: #7D7D7D;"> Ученик </div>
          </q-card-section>

          <q-card-section>
            <div class="row">
              <q-item class="contacts"> Номер телефона: </q-item>
              <q-item class="contact-info"> {{ phoneNumber }}</q-item>
            </div>
            <div class="row">
              <q-item dense class="contacts"> Электронная почта: </q-item>
              <q-item dense class="contact-info">
                <p></p>{{ autorizeEmail }}
              </q-item>
            </div>
          </q-card-section>

          <q-card-actions vertical align="center">
            <editProfile-button class="user-buttons" @autorize="getUserData" />
          </q-card-actions>
        </q-card>
      </div>

      <div class="column" style="width: 45%; margin-left: 40px;">
        <q-card class="my-card" flat bordered>
          <q-card-section>
            <q-item style="font-size: 22px; font-weight: 600; color: #000000;"> Мои курсы </q-item>
          </q-card-section>
          <q-card-section>
            <course-card class="list-card" v-for="course in currentCourses" :data="course" />
          </q-card-section>

          <q-card-actions vertical>
            <q-btn no-caps outline style="align-self: center;" class="user-buttons">
              Перейти к курсам
            </q-btn>
          </q-card-actions>
        </q-card>
      </div>

    </div>

    <div class="row" style="margin-bottom: 20px;">
      <div class="column" style="width: 45%;">
        <q-card class="my-card" flat bordered>
          <q-card-section>
            <q-item style="font-size: 22px; font-weight: 600; color: #000000;"> Настройки </q-item>
          </q-card-section>
          <q-card-actions vertical align="center">
            <editPassword-button class="user-buttons"/>
          </q-card-actions>
        </q-card>
      </div>
    </div>

    <div align="center" style="margin-bottom: 20px;">
      <q-btn no-caps outline class="exit-btn" @click="unautorize">
        Выйти
      </q-btn>
    </div>
  </q-page-container>
</template>

<script>
import { defineComponent } from "vue";
import { fetchUserData, getCurrentCourses, fetchlogout } from "boot/axios";
import { fetchLoginedUserData } from 'boot/axios'
import EditProfileButton from 'components/User/EditProfileButton.vue'
import EditPasswordButton from 'components/User/EditPasswordButton.vue'
import CourseCard from "components/Courses/CourseCard.vue";

export default defineComponent({
  name: "UserInfoPage",
  components: {
    EditProfileButton,
    EditPasswordButton,
    CourseCard
  },
  data() {
    return {
      data: [],
      autorizeEmail: "",
      phoneNumber: "",
      firstName: "",
      middleName: "",
      lastName: "",
      instagramLink: "",
      citizenship: "",
      currentCourses: [],
      logined: true,
    };
  },
  methods: {
    async getUserData() {
      try {
        const autorize = await fetchLoginedUserData();
        this.autorizeEmail = autorize.data.value.email
        const response = await fetchUserData(this.autorizeEmail);
        this.data = response.data.value;
        this.firstName = response.data.value.firstName
        this.lastName = response.data.value.lastName
        this.middleName = response.data.value.middleName
        this.phoneNumber = response.data.value.phoneNumber
        this.instagramLink = response.data.value.instagramLink
        this.citizenship = response.data.value.citizenship
      } catch (error) {
        console.log(error.message);
        this.$router.push({ name: 'homepage' })
      }
    },
    async getCoursesData() {
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
    unautorize: function () {
      fetchlogout();
      this.logined = false;
      this.autorizeEmail = ""
      window.location.reload();
    },
  },
  mounted() {
    this.getUserData();
    this.getCoursesData();
  },
  updated() {
    this.getUserData();
    this.getCoursesData();
  }
});
</script>

<style>
.user-buttons {
  width: 80%;
  color: #0375DF;
  font-size: 16px;
  font-weight: 500;
  border-radius: 10px;
}
</style>

<style scoped>
.my-card {
  width: 100%;
  max-width: 100%;
  border-radius: 12px;
  box-shadow: 0px 4px 33px rgba(0, 0, 0, 0.12);
}

.user {
  width: 50%;
}

.user-data {
  display: flex;
  flex-direction: column;
}

.contact-info {
  width: 50%;
  font-size: 16px;
  font-weight: 300;
  color: #000000;
}

.contacts {
  width: 50%;
  font-size: 16px;
  font-weight: 300;
  color: #7D7D7D;
}

.exit-btn {
  color: #CD3838;
  font-size: 16px;
  font-weight: 500;
  border-radius: 10px;
}
</style>
