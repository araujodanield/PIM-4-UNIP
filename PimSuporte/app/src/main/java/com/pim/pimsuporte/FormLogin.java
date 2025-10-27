package com.pim.pimsuporte;

import android.content.Intent;
import android.os.Bundle;
import android.view.View; // Importação necessária para View.OnClickListener
import android.widget.Button;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class FormLogin extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_form_login);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });


        Button botaoLogin = findViewById(R.id.button);


        botaoLogin.setOnClickListener(v -> {


            Toast.makeText(FormLogin.this, "Login realizado com sucesso!", Toast.LENGTH_SHORT).show();


            Intent intent = new Intent(FormLogin.this, Tela_Inicial.class);
            startActivity(intent);


            finish();
        });

    }

}