package com.pim.pimsuporte.API;

import android.os.Build;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeUnit;
import okhttp3.ConnectionSpec;
import okhttp3.OkHttpClient;
import okhttp3.TlsVersion;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class retrofitclient {

    private static final String BASE_URL = "https://apipim-anfwgmdah3fre6ca.brazilsouth-01.azurewebsites.net/";

    private static Retrofit retrofit = null;

    private static Retrofit getClient() {
        if (retrofit == null) {

            // Configuração do Gson
            Gson gson = new GsonBuilder()
                    .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                    .setLenient()
                    .create();

            // Configuração de logs para debug (veja as requisições no Logcat)
            HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
            logging.setLevel(HttpLoggingInterceptor.Level.BODY);

            // Configuração do OkHttpClient
            OkHttpClient.Builder clientBuilder = new OkHttpClient.Builder()
                    .connectTimeout(30, TimeUnit.SECONDS)
                    .readTimeout(30, TimeUnit.SECONDS)
                    .writeTimeout(30, TimeUnit.SECONDS)
                    .addInterceptor(logging); // Interceptor adicionado aqui

            // CORREÇÃO PARA LOLLIPOP - Habilita TLS 1.2
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP
                    && Build.VERSION.SDK_INT < Build.VERSION_CODES.LOLLIPOP_MR1) {

                try {
                    // ... (lógica de correção de TLS mantida)
                    TLSSocketFactory tlsSocketFactory = new TLSSocketFactory();

                    if (tlsSocketFactory.getTrustManager() != null) {
                        clientBuilder.sslSocketFactory(tlsSocketFactory, tlsSocketFactory.getTrustManager());
                    }

                    // Configuração de specs de conexão
                    ConnectionSpec cs = new ConnectionSpec.Builder(ConnectionSpec.MODERN_TLS)
                            .tlsVersions(TlsVersion.TLS_1_2)
                            .build();

                    List<ConnectionSpec> specs = new ArrayList<>();
                    specs.add(cs);
                    specs.add(ConnectionSpec.COMPATIBLE_TLS);
                    specs.add(ConnectionSpec.CLEARTEXT);

                    clientBuilder.connectionSpecs(specs);

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }

            OkHttpClient client = clientBuilder.build();

            // Configuração do Retrofit
            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .client(client)
                    .addConverterFactory(GsonConverterFactory.create(gson))
                    .build();
        }
        return retrofit;
    }

    public static API getApiService() {
        return getClient().create(API.class);
    }
}